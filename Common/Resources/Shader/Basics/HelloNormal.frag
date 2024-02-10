#version 330 core

out vec4 outputColor;

in VS_OUT
{
    vec3 color;
    vec2 uv;
    vec3 tangentLightPos;
    vec3 tangentViewPos;
    vec3 tangentFragPos;
} fs_in;

uniform sampler2D diffuseMap;
uniform sampler2D normalMap;

uniform vec3 lightColor;

void main()
{
    // obtain normal from normal map in range [0,1]
    vec3 normal = texture(normalMap, fs_in.uv).rgb;
    // transform normal vector to range [-1,1]
    normal = normalize(normal * 2.0 - 1.0); // this normal is in tangent space

    // Get the difusse color from texture
    vec3 diffuseColor = texture(diffuseMap, fs_in.uv).rgb;

    // Diffuse lighting
    vec3 lightDir = normalize(fs_in.tangentLightPos - fs_in.tangentFragPos.xyz);
    float diffuseLight = max(dot(normal, lightDir), 0.0);
    vec3 diffuse = diffuseColor * diffuseLight;

    // Specular lighting
    float specularStrength = 0.24;
    vec3 viewDir = normalize(fs_in.tangentViewPos - fs_in.tangentFragPos.xyz);
    vec3 reflectDir = reflect(lightDir, normal);
    vec3 halfwayDir = normalize(lightDir + viewDir);  
    float specular = pow(max(dot(normal, halfwayDir), 0.0), 12.0);

    outputColor = vec4(diffuse + (specular * specularStrength), 1.0);
}