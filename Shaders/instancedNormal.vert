#version 430 core

layout (location = 0) in vec3 aPosition;
layout (location = 1) in vec3 aNormal;

layout(std430, binding = 0) buffer models {
	mat4 model[];
};

layout(std430, binding = 1) buffer normalMats {
	mat4 normalMat[];
};

uniform mat4 view;
uniform mat4 projection;

out vec3 normal;
out vec3 FragPos;

void main() {
	gl_Position = vec4(aPosition, 1.0) * model[gl_InstanceID] * view * projection;
	
	FragPos = vec3(vec4(aPosition, 1.0) * model[gl_InstanceID]);
	normal = aNormal * mat3(normalMat[gl_InstanceID]);
}