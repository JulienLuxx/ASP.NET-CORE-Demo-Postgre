FROM mcr.microsoft.com/dotnet/core/aspnet:2.2
LABEL author="julienlux@outlook.com"
WORKDIR /workspace/MyTestDemo
COPY *.* ./workspace/MyTestDemo
# RUN dotnet restore
# RUN dotnet publish -o/out -c Release
EXPOSE 8089
ENTRYPOINT ["dotnet","Test.Web.dll"]