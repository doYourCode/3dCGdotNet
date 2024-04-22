#version 420 core

out vec4 FragColor;
in vec2 texCoords;

layout (binding = 0) uniform sampler2D fboTexture;

uniform float gamma;

void main()
{
    vec4 fragment = texture(fboTexture, texCoords);

    vec4 pebFrag = vec4(fragment.r, fragment.r, fragment.r, 1.0f);

    vec4 vintageFrag = pebFrag * vec4(1.05f, 0.57f, 0.23f, 1.0f);

    vec4 mexicoFilterFrag = pebFrag * vec4(1.05f, 0.93f, 0.39f, 1.0f);

    vec4 coldFilterFrag = pebFrag * vec4(0.45f, 0.93f, 1.0f, 1.0f);

    float x = 0.0f;

    if (fragment.r + fragment.g + fragment.b > 2.5f)
    {
        x = 1.0f;
    }

    vec4 mask = vec4(x, x, x, 1.0f);

    ivec2 size = textureSize(fboTexture, 0);

    float uv_x = texCoords.x * size.x;
    float uv_y = texCoords.y * size.y;

    vec2 blur_size = vec2(2.5, 2.5);

    int num = 9;

    vec4 sum = vec4(0.0);
    for (int n = 0; n < num; ++n)
    {
        uv_y = (texCoords.y * size.y) + (blur_size.y * float(n - 4.5));
        vec4 h_sum = vec4(0.0);
        h_sum += texelFetch(fboTexture, ivec2(uv_x - (4.0 * blur_size.x), uv_y), 0);
        h_sum += texelFetch(fboTexture, ivec2(uv_x - (3.0 * blur_size.x), uv_y), 0);
        h_sum += texelFetch(fboTexture, ivec2(uv_x - (2.0 * blur_size.x), uv_y), 0);
        h_sum += texelFetch(fboTexture, ivec2(uv_x - blur_size.x, uv_y), 0);
        h_sum += texelFetch(fboTexture, ivec2(uv_x, uv_y), 0);
        h_sum += texelFetch(fboTexture, ivec2(uv_x + blur_size.x, uv_y), 0);
        h_sum += texelFetch(fboTexture, ivec2(uv_x + (2.0 * blur_size.x), uv_y), 0);
        h_sum += texelFetch(fboTexture, ivec2(uv_x + (3.0 * blur_size.x), uv_y), 0);
        h_sum += texelFetch(fboTexture, ivec2(uv_x + (4.0 * blur_size.x), uv_y), 0);
        sum += h_sum / 9.0;
    }

    sum = sum / num;

    FragColor = sum;
}