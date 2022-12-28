SCRIPTS[0]="/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P a?i0/cEFB@v3dweF7C -d master -i Identity-create-db.sql"
SCRIPTS[1]="/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P a?i0/cEFB@v3dweF7C -d FitnessIdentity -i Identity-create-tables.sql"
SCRIPTS[2]="/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P a?i0/cEFB@v3dweF7C -d FitnessIdentity -i Identity-create-storedProcedures.sql"
SCRIPTS[3]="/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P a?i0/cEFB@v3dweF7C -d FitnessIdentityTests -i Identity-create-tables.sql"
SCRIPTS[4]="/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P a?i0/cEFB@v3dweF7C -d FitnessIdentityTests -i Identity-create-storedProcedures.sql"
SCRIPTS[5]="/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P a?i0/cEFB@v3dweF7C -d master -i EventStore-create-db.sql"
SCRIPTS[6]="/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P a?i0/cEFB@v3dweF7C -d FitnessEventStore -i EventStore-create-tables.sql"
SCRIPTS[7]="/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P a?i0/cEFB@v3dweF7C -d FitnessEventStore -i EventStore-create-storedProcedures.sql"
SCRIPTS[8]="/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P a?i0/cEFB@v3dweF7C -d FitnessEventStoreTests -i EventStore-create-tables.sql"
SCRIPTS[9]="/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P a?i0/cEFB@v3dweF7C -d FitnessEventStoreTests -i EventStore-create-storedProcedures.sql"
SCRIPTS[10]="/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P a?i0/cEFB@v3dweF7C -d FitnessExercises -i Exercise-create-db.sql"
SCRIPTS[11]="/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P a?i0/cEFB@v3dweF7C -d FitnessExercises -i Exercise-create-tables.sql"
SCRIPTS[12]="/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P a?i0/cEFB@v3dweF7C -d FitnessExercises -i Exercise-create-storedProcedures.sql"


for ((i = 0; i < ${#SCRIPTS[@]}; i++))
do   
    echo "start"
    for x in {1..30};
    do
        ${SCRIPTS[$i]};  
        if [ $? -eq 0 ]
        then
            echo "Operation number ${i} completed"
            break
        else
            echo "Operation number ${i} not ready yet..."
            sleep 1
        fi
    done 
done 