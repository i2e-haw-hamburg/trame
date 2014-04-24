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
        line(joint_point[0], -joint_point[1], joint_point[2], 
            joint['normal'][0] + joint_point[0],
            -(joint['normal'][1] + joint_point[1]),
            joint['normal'][2] + joint_point[2])
    if not start:
        line(joint_point[0], -joint_point[1], joint_point[2], 
            parent_point[0], -parent_point[1], parent_point[2])
    
    point(joint_point[0], -joint_point[1], joint_point[2])
    for c in joint['children']:
        display_joint(c, joint_point)


def draw():
    """
    Animate a 3D context free plant in processing/pyglet draw loop
    """
    global dim
    cmd = '../../build/trame-viewer'
    skeleton = json.loads(check_output([cmd]))

    background(210, 210, 210)
    lights()  
    camera(width/2.0, -1400, -2000, 0, -1000, 0, 0, 1, 0) 
    speedRotation(4.5)
    pushMatrix()

    textAlign(CENTER)
    text("ID: %d" % (skeleton['id']), 0, 60)
    text("Time: %d" % (skeleton['timestamp']), 0, 80);

    display_joint(skeleton['root'], [0,0,0], True)

    popMatrix()

run()  
