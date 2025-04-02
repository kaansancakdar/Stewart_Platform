import numpy as np
import math as Math

def deg2rad(deg):
    rad=deg*np.pi/180
    return  rad

def rad2deg (rad):
    deg=rad*180/np.pi
    return deg

def rotMat(roll, pitch, yaw):

    roll=np.deg2rad(roll)
    pitch=np.deg2rad(pitch)
    yaw=np.deg2rad(yaw)

    rot_mat=np.zeros((3,3))
    rot_mat[0, 0] =  Math.cos(yaw)  * Math.cos(pitch)
    rot_mat[0, 1] = -Math.sin(yaw)  * Math.cos(roll) + Math.cos(yaw) * Math.sin(pitch) * Math.sin(roll)
    rot_mat[0, 2] =  Math.sin(yaw)  * Math.sin(roll) + Math.cos(yaw) * Math.sin(pitch) * Math.cos(roll)
    rot_mat[1, 0] =  Math.sin(yaw)  * Math.cos(pitch)
    rot_mat[1, 1] =  Math.cos(yaw)  * Math.cos(roll) + Math.sin(yaw) * Math.sin(pitch) * Math.sin(roll)
    rot_mat[1, 2] = -Math.cos(yaw)  * Math.sin(roll) + Math.sin(yaw) * Math.sin(pitch) * Math.cos(roll)
    rot_mat[2, 0] = -Math.sin(pitch)
    rot_mat[2, 1] =  Math.cos(pitch) * Math.sin(roll)
    rot_mat[2, 2] =  Math.cos(pitch) * Math.cos(roll)

    return rot_mat

def point2coordinate(angle, radius, height):

    coordinate=np.zeros((6,3))

    count=0

    for i in angle:
        coordinate[count,0]=radius*Math.cos(deg2rad(i))
        coordinate[count,1]=radius*Math.sin(deg2rad(i))
        coordinate[count,2]=height
        count=count+1
    
    return coordinate

def linkLength(roll, pitch, yaw, Blist, Plist,TVector):

    l_List = np.zeros((6, 3)) #Link vectors matrix
    link_length=np.zeros(6) #Link lengths vector

    for i in range(6):
        aaa = np.matmul(rotMat(roll,pitch,yaw), Plist[i]).T
        bb = TVector + aaa - Blist[i]
        l_List[i]=bb

    count=0
    for i in l_List:
        link_length[count]=Math.sqrt(Math.pow(i[0],2)+Math.pow(i[1],2)+Math.pow(i[2],2))
        count=count+1

    return link_length

def platformPointsRotation(roll, pitch, yaw,Plist):
    platformPoints = np.zeros((6, 3)) #Link vectors matrix
    for i in range(6):
        platformPoints[i] = np.matmul(rotMat(roll,pitch,yaw), Plist[i]).T
    return platformPoints

def calculateAlpha(betha, roll, pitch, yaw, Plist, Blist, servoArmLength, servoLinkLength, TVector):
    alpha =np.zeros(6)
    Alpha=np.zeros(6)
    beta=np.zeros(6)
    L=np.zeros(6)
    M=np.zeros(6)
    N=np.zeros(6)
    s=servoLinkLength
    a=servoArmLength
    #print("Servo Link Length=",s)
    #print("Servo Arm Length=",a)
    
    beta=np.deg2rad(betha)
    betha_c=np.cos(betha)
    betha_s=np.sin(betha)
    #print("Betha List=",np.around (betha,decimals=2))

    bi=Blist
    #print("B List=\n",np.around(bi,decimals=2))
    pi=platformPointsRotation(roll, pitch, yaw,Plist)
    #print("P List=\n",np.around(pi,decimals=2))
    Li=linkLength(roll, pitch, yaw,Blist,Plist,TVector)
    #print("Link Length=\n",np.around(Li,decimals=2))
    
    for i in range(6):

        L[i]=np.power(Li[i],2)-np.power(s,2)+np.power(a,2)
        M[i]=2*a*(pi[i,2]-bi[i,2])
        N[i]=2*a*((np.cos(beta[i])*(pi[i,0]-bi[i,0]))+(np.sin(beta[i])*(pi[i,1]-bi[i,1])))
        
        alpha[i]=np.arcsin(L[i]/np.sqrt(np.power(M[i],2)+np.power(N[i],2)))-np.arctan2(N[i],M[i])
    
    #print("L",np.around (L,decimals=2))
    #print("M",np.around (M,decimals=2))
    #print("N",np.around (N,decimals=2))
    for i in range(6):
        Alpha[i]=np.rad2deg(alpha[i])

    return Alpha