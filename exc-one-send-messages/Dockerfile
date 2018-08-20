FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["exc-one-send-messages/exc-one-send-messages.csproj", "exc-one-send-messages/"]
RUN dotnet restore "exc-one-send-messages/exc-one-send-messages.csproj"
COPY . .
WORKDIR "/src/exc-one-send-messages"
RUN dotnet build "exc-one-send-messages.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "exc-one-send-messages.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "exc-one-send-messages.dll"]