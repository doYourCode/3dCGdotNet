#version 330 core

in vec3 vColor;
in vec2 vUv;
in vec4 vNormal;
in vec4 fragPosition;

out vec4 outputColor;

uniform sampler2D texture0;

uniform vec3 lightPosition;
uniform vec3 lightDirection;
uniform vec3 lightColor;
uniform float lightIntensity;
uniform vec3 ambientColor;
uniform float ambientIntensity;
uniform vec3 viewPosition;

uniform float roughness;

float OrenNayarTerm(vec3 ViewDirection, vec3 LightDirection, vec3 NormalDirection)
{
    float NdotL = dot(NormalDirection, LightDirection);
    float angleLN = acos(NdotL);

    float NdotV = dot(NormalDirection, ViewDirection);
    float angleVN = acos(NdotV);

    float alpha = max(angleVN, angleLN);
    float beta = min(angleVN, angleLN);
    float gamma = cos(angleVN - angleLN);

    float roughness2 = roughness * roughness;

    float A = 1.0 - 0.5 * (roughness2 / (roughness2 + 0.57));
    float B = 0.45 * (roughness2 / (roughness2 + 0.09));
    float C = sin(alpha) * tan(beta);

    float OrenNayar = NdotL * (A + (B * max(0.0, gamma) * C));

    return max(OrenNayar, 0.0);
}

void main()
{
    vec3 lightDir = normalize(lightPosition - fragPosition.xyz);
    vec3 viewDir = normalize(-viewPosition - fragPosition.xyz);
    vec3 normal = normalize(vNormal.xyz);

    vec3 diffuseLight = OrenNayarTerm(viewDir, lightDir, normal) * lightColor * lightIntensity;

    vec3 textureColor = texture(texture0, vUv).xyz;
    
    vec3 diffuseFinalColor = (diffuseLight + ambientColor * ambientIntensity) * textureColor;

    outputColor = vec4(diffuseFinalColor, 1.0f);
}
