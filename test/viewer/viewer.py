#!/usr/bin/python
"""

"""
from pyprocessing import *
from subprocess import check_output
from math import pi, sin
from time import time

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
    size(800, 600)
    rectMode(CENTER)
    rotateY(pi/2)
    fill(0, 200, 0)
    strokeWeight(5)
    stroke(255,0,0) #red color  

def draw():
    """
    Animate a 3D context free plant in processing/pyglet draw loop
    """
    global dim
    cmd = '../../build/trame-viewer'
    #skeleton = json.loads(check_output([cmd]))

    background(20, 20, 180)
    lights()  
    camera(width/2, height/2, (height/2) / tan(PI/6), 0, 0, 0, 0, 1, 0)     
    speedRotation(4.5)
    pushMatrix()

    textAlign(CENTER);
    text("This text is centered.",0,60); 
    
    point(0,dim,dim)
    point(0,dim,0)
    #point(0,0,dim)
    point(0,0,0)

    popMatrix()

run()  
