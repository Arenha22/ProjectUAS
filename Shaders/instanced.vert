#version 430 core

layout (location = 0) in vec3 aPosition;

layout(std430, binding = 0) buffer models {
	mat4 model[];
};

uniform mat4 view;
uniform mat4 projection;

void main() {
	gl_Position = vec4(aPosition, 1.0) * model[gl_InstanceID] * view * projection;
}