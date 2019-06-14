#
# Dockerfile for partytalk app
#
# The source code can be found at https://github.com/Tomas-Perez/acs
#

# Pull core image
FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS publish

# Env variables
ENV DB_SERVER localhost

# Install git, clone the repository and compile the project
RUN \
  apt install git && \
  git clone https://github.com/Tomas-Perez/acs.git && \
  cd acs/acs && \
  dotnet publish acs.csproj -c Release -o /app

# Pull runtime image  
FROM mcr.microsoft.com/dotnet/core/runtime:2.2

WORKDIR /app

EXPOSE 1234

COPY --from=publish /app .

ENTRYPOINT dotnet acs.dll $DB_SERVER