// (c) Robert Eisele
// https://www.xarg.org/paper/inverse-kinematics-of-a-stewart-platform/

// Creates a Stewart Platform plate defined by an inner and outer radius
r = 50; // Radius of inscribed circle of smaller triangle
s = 60; // Radius of inscribed circle of larger triangle

polygon([ for (
		i   = [0 : 6], 
		ap  = pow(-1, i) * (2 * r - s) / sqrt(3), 
		phi = 120 * floor(i / 2)) [
   s * cos(phi) + ap * sin(phi), 
   s * sin(phi) - ap * cos(phi)]]);



