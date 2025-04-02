import numpy as np
import stw_calculations as sc

import os
os.system('cls')

#---------------------------------
#-------Platform Parameters-------
base_radius=125#Base servo arm rotation point radius
platform_radius=99.943# Platform connection joit radius
z0=147.146 #Home heigth with no translation and rotation

a=45 #Servo arm length between rotation pint to joint point
s=172 #Servo link length between joint points
betha=np.array([180,0,300,120,60,240]) #Servo arm angle relative to x-axis

B_Angle=np.array([65,115,185,235,305,355]) #Servo arm rotation point relative to x-axis
P_Angle=np.array([37.25, 142.75, 157.25, 262.75, 277.25, 22.75]) #Platform connection angle relative to x-axis
#---------------------------------

#---------------------------------
#---------Desired Position--------
Translation_Vector=np.array([0,0,10])
roll=0
pitch=0
yaw=0
#---------------------------------

B_List=sc.point2coordinate(B_Angle,base_radius,0) #Calculate servo rotation points
P_List=sc.point2coordinate(P_Angle,platform_radius,z0) #Calculate platform connection points

alphList=sc.calculateAlpha(betha, roll,pitch,yaw, P_List, B_List, a, s, Translation_Vector) #Calculate servo angles
print("Servo Arm Angles:\n",np.around(alphList,decimals=3))