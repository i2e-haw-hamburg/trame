#!/usr/bin/python
from pyprocessing import *
from subprocess import check_output
import json

def setup():
  size(1000,600)
  background(255)
  strokeWeight(5)
  smooth()
  frameRate(25)
  
  stroke(0, 30)
  noFill()  

def draw():
  cmd = '../../build/trame-viewer'
  skeleton = json.loads(check_output([cmd]))

  # show timestamp and id

  # go threw tree and display points

  ##end def draw()
   
run()