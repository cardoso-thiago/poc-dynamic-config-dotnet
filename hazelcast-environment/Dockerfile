FROM python:3.11-slim

WORKDIR /app

RUN apt-get update && apt-get install -y \
    curl \
    && rm -rf /var/lib/apt/lists/*

COPY python_client.py /app/python_client.py
RUN pip install hazelcast-python-client flask

CMD ["python", "/app/python_client.py"]