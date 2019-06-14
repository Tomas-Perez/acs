#
# Dockerfile for partytalk app
#
# The source code can be found at https://github.com/Tomas-Perez/acs
#

# Pull base image
FROM mcr.microsoft.com/dotnet/core/sdk:2.2

# Env variables
ENV DB_SERVER localhost

# Install git, clone the repository and compile the project
RUN \
  apt install git && \
  git clone https://github.com/Tomas-Perez/acs.git

# Define working directory
WORKDIR /acs/acs

ENTRYPOINT \
  git pull && \
  dotnet run DB_SERVER