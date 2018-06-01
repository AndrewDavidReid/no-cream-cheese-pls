FROM adr-vdatastorage:5000/fluent-migrator as base
WORKDIR /app

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
ARG MIGRATIONS_PROJECT
COPY ${MIGRATIONS_PROJECT}/${MIGRATIONS_PROJECT}.csproj ${MIGRATIONS_PROJECT}/
RUN dotnet restore ${MIGRATIONS_PROJECT}/${MIGRATIONS_PROJECT}.csproj
COPY . .
WORKDIR /src/${MIGRATIONS_PROJECT}
RUN dotnet build ${MIGRATIONS_PROJECT}.csproj -c Release -o /app

FROM build AS publish
ARG MIGRATIONS_PROJECT
RUN dotnet publish ${MIGRATIONS_PROJECT}.csproj -c Release -o /app

FROM base as final
WORKDIR /app
ARG MIGRATIONS_PROJECT
COPY --from=publish /app .
ENV DLL_FILE_NAME ${MIGRATIONS_PROJECT}.dll
ENTRYPOINT dotnet fm migrate -p $DATABASE_PROVIDER -c $CONNECTION_STRING -a $DLL_FILE_NAME -t $DEPLOYMENT_ENVIRONMENT
