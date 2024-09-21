FROM python:3.11-slim

WORKDIR /app

COPY populate_map.py /app/populate_map.py

RUN pip install hazelcast-python-client

CMD ["python", "/app/populate_map.py"]