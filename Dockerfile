FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["src/WorkTimeCalculator.Wasm/WorkTimeCalculator.Wasm.csproj", "src/WorkTimeCalculator.Wasm/"]
RUN dotnet restore "src/WorkTimeCalculator.Wasm/WorkTimeCalculator.Wasm.csproj"

COPY . .
RUN dotnet publish "src/WorkTimeCalculator.Wasm/WorkTimeCalculator.Wasm.csproj" -c Release -o /app/publish

FROM nginx:alpine AS final
WORKDIR /usr/share/nginx/html
COPY --from=build /app/publish/wwwroot .
COPY nginx.conf /etc/nginx/nginx.conf
EXPOSE 80
