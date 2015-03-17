#!/usr/bin/python
"""

"""
from pyprocessing import *
from math import pi, sin
from time import time
import json
import urllib2

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
        return

    if not (joint['normal'] is None):
        stroke(0,255,0)
        line(joint_point[0], -joint_point[1], joint_point[2], 
            (joint['normal'][0] + joint_point[0]),
            -(joint['normal'][1] + joint_point[1]),
            (joint['normal'][2] + joint_point[2]))
        stroke(255,0,0)

    if not start:
        line(joint_point[0], -joint_point[1], joint_point[2], 
            parent_point[0], -parent_point[1], parent_point[2])
    
    point(joint_point[0], -joint_point[1], joint_point[2])

    for c in joint['children']:
        display_joint(c, joint_point)


def get_skeleton():
    server_path = "http://localhost:12345/"
    json_string = urllib2.urlopen(server_path)
    json_data = json.load(json_string)
    return json_data


def draw():
    """
    Animate a 3D context free plant in processing/pyglet draw loop
    """
    global dim

    background(210, 210, 210)
    lights()  

    camera(width/2.0, -1400, -2000, 0, -1000, 0, 0, 1, 0) 
    speedRotation(4.5)
    pushMatrix()
    textAlign(CENTER)

    try:
        skeleton = get_skeleton()
        text("ID: %d" % (skeleton['id']), 0, 60)
        text("Time: %d" % (skeleton['timestamp']), 0, 80);

        display_joint(skeleton['root'], [0,0,0], True)
    except urllib2.URLError:
        pass
    
    popMatrix()

run()  
