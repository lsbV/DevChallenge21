# Use an appropriate base image
FROM nvidia/cuda:12.0.0-cudnn8-runtime-ubuntu20.04

# Set the environment variable to avoid interactive prompts
ENV DEBIAN_FRONTEND=noninteractive

# Install ffmpeg and python3-pip
RUN apt-get update && apt-get install -y \
    ffmpeg \
    python3-pip \
    tzdata  # This is to install tzdata non-interactively

# Rest of your Dockerfile
WORKDIR /app

COPY requirements.txt .

RUN pip3 install --no-cache-dir -r requirements.txt

COPY . .

EXPOSE 5000

CMD ["python3", "app.py"]