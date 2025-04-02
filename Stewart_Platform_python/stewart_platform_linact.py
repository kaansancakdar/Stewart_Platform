import numpy as np
import stw_calculations as sc

import os
os.system('cls')


base_radius=125
platform_radius=99.94
z0=193.16

s=202

B_Angle=np.array([65,115,185,235,305,355])
P_Angle=np.array([37.25, 142.75, 157.25, 262.75, 277.25, 22.75])

Translation_Vector=np.array([0,0,0])
roll=0
pitch=0
yaw=0

B_List=sc.point2coordinate(B_Angle,base_radius,0) #According to base frame
P_List=sc.point2coordinate(P_Angle,platform_radius,z0) #According to platform frame

print("B_List=\n",np.around(B_List,decimals=2))
print("P_List=\n",np.around(P_List,decimals=2))

platform_Points=sc.platformPointsRotation(roll,pitch,yaw,P_List)
print("Platform Joint Coordinates:\n",np.around(platform_Points,decimals=2))

link_length = sc.linkLength(roll,pitch,yaw,B_List,P_List,Translation_Vector)
print("Link Lengths=\n",np.around(link_length,decimals=2))
link_length_1=link_length-s
print("link_length_1=\n",np.around(link_length_1,decimals=2))
