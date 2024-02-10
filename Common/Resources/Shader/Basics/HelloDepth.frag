#version 330 core

out vec4 outputColor;

float linearizeDepth(float depth, float near, float far)
{
	return (2.0 * near * far) / (far + near - (depth * 2.0 - 1.0) * (far - near));
}

float logisticDepth(float depth, float steepness = 0.5f, float offset = 5.0f)
{
	float zVal = linearizeDepth(depth, 0.1, 100);
	return (1 / (1 + exp(-steepness * (zVal - offset))));
}

void main()
{
    float depth = linearizeDepth(gl_FragCoord.z, 0.1, 100) / 100;
	outputColor = vec4(vec3(depth), 1.0);
}