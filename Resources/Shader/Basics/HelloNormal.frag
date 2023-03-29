#version 400

in vec3 vColor;
in vec2 vUv;
in vec3 TangentLightPos;
in vec3 TangentViewPos;
in vec3 TangentFragPos;

out vec4 outputColor;

uniform sampler2D texture0;
uniform sampler2D texture1;

uniform vec3 lightColor;

void main()
{
    vec3 imgNormal = texture(texture1, vUv).xyz;
    vec3 rangedNormal = imgNormal * 2.0 - 1.0;   
    // Diffuse lighting
    vec3 norm = normalize(rangedNormal);
    vec3 lightDir = normalize(TangentLightPos - TangentFragPos.xyz);
    float diffuse = max(dot(norm, lightDir), 0.0);

    // Specular lighting
    float specularStrength = 0.16;
    vec3 viewDir = normalize(-TangentViewPos - TangentFragPos.xyz);
    vec3 reflectDir = reflect(lightDir, norm);
    float spec = pow(max(dot(viewDir, reflectDir), 0.0), 8);

    outputColor = texture(texture0, vUv) * diffuse + spec * specularStrength;
}