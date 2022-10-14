FROM mcr.microsoft.com/mssql/server
 
USER root

ARG PROJECT_DIR=/tmp/mssql-scripts

RUN mkdir -p $PROJECT_DIR
WORKDIR $PROJECT_DIR
ENV SA_PASSWORD=a?i0/cEFB@v3dweF7C
ENV ACCEPT_EULA=Y
COPY Identity-create-db.sql ./
COPY Identity-create-tables.sql ./
COPY Identity-create-storedProcedures.sql ./
COPY EventStore-create-db.sql ./
COPY EventStore-create-tables.sql ./
COPY EventStore-create-storedProcedures.sql ./
COPY entrypoint.sh ./
COPY setupsql.sh ./

RUN chmod +x setupsql.sh
CMD ["/bin/bash", "entrypoint.sh"]