set TOOLS_PATH=%userprofile%\.nuget\packages\grpc.tools\1.10.0\tools\windows_x64

%TOOLS_PATH%\protoc.exe -I./protos --csharp_out . ./protos/testService.proto --grpc_out . --plugin=protoc-gen-grpc=%TOOLS_PATH%\grpc_csharp_plugin.exe
