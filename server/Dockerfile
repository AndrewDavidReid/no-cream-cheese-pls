FROM microsoft/dotnet:2.1-sdk
WORKDIR /src
COPY . .
ARG TESTS_PROJECT
ENV tests_project ${TESTS_PROJECT}/${TESTS_PROJECT}.csproj
RUN dotnet restore ${tests_project}
ENTRYPOINT dotnet test ${tests_project}
