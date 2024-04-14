#version 420 core

out vec4 outColor;

void main()
{
	//outColor = vec4(vec3(gl_FragDepth), 1.0f);
	outColor = vec4(vec3(gl_FragDepth), 1.0f);
}