#!/usr/bin/python
"""

"""
from pyprocessing import *
from subprocess import check_output
from math import pi, sin
from time import time
import json

angle = 0
dim = 200
        
def speedRotation(speed):

    global angle
    angle = angle + 0.01
    rotateY(angle)

def setup():
    """
    processing setup
    """

    size(1200, 1200)
    
    rectMode(CENTER)
    fill(0, 200, 0)
    strokeWeight(5)
    stroke(255,0,0) #red color  
    textSize(32)

def display_joint(joint, parent_point, start = False):
    if not (joint['point'] is None):
        joint_point = [joint['point'][0] + parent_point[0], 
            joint['point'][1] + parent_point[1], 
            joint['point'][2] + parent_point[2]]
    else:
        joint_point = [0,0,0]

    if not (joint['normal'] is None):
        stroke(0,255,0)
        line(joint_point[0], -joint_point[1], joint_point[2], 
            joint['normal'][0] + joint_point[0],
            -(joint['normal'][1] + joint_point[1]),
            joint['normal'][2] + joint_point[2])
        stroke(255,0,0)

    if not start:
        line(joint_point[0], -joint_point[1], joint_point[2], 
            parent_point[0], -parent_point[1], parent_point[2])
    
    point(joint_point[0], -joint_point[1], joint_point[2])

    for c in joint['children']:
        display_joint(c, joint_point)


def get_skeleton():
    json_string = '{"id":0,"root":{"children":[{"children":[{"children":[],"normal":[0,0,10],"point":[0,180,0],"type":1},{"children":[{"children":[{"children":[{"children":[],"normal":[-10,0,0],"point":[50,475,0],"type":124}],"normal":null,"point":[-50,-455,0],"type":123}],"normal":null,"point":[75,-320,0],"type":122}],"normal":null,"point":[-220,0,0],"type":121},{"children":[{"children":[{"children":[{"children":[],"normal":[10,0,0],"point":[50,-475,0],"type":114}],"normal":null,"point":[50,-455,0],"type":113}],"normal":null,"point":[-75,-320,0],"type":112}],"normal":null,"point":[220,0,0],"type":111}],"normal":null,"point":[0,350,0],"type":5},{"children":[{"children":[{"children":[{"children":[],"normal":[0,0,-10],"point":[-20,0,-255],"type":224}],"normal":null,"point":[0,-410,0],"type":223}],"normal":null,"point":[0,-540,0],"type":222}],"normal":null,"point":[180,-100,0],"type":221},{"children":[{"children":[{"children":[{"children":[],"normal":[0,0,10],"point":[-20,0,-255],"type":214}],"normal":null,"point":[0,-410,0],"type":213}],"normal":null,"point":[0,-540,0],"type":212}],"normal":null,"point":[-180,-100,0],"type":211}],"normal":[0,0,10],"point":[0,1100,0],"type":9},"timestamp":4215765565}'
    cmd = '../../build/trame-viewer'
    return json.loads(check_output([cmd]))


def draw():
    """
    Animate a 3D context free plant in processing/pyglet draw loop
    """
    global dim
    
    skeleton = get_skeleton()

    background(210, 210, 210)
    lights()  

    camera(width/2.0, -1400, -2000, 0, -1000, 0, 0, 1, 0) 
    speedRotation(4.5)
    pushMatrix()

    textAlign(CENTER)
    #text("ID: %d" % (skeleton['id']), 0, 60)
    #text("Time: %d" % (skeleton['timestamp']), 0, 80);

    display_joint(skeleton['root'], [0,0,0], True)

    popMatrix()

run()  
