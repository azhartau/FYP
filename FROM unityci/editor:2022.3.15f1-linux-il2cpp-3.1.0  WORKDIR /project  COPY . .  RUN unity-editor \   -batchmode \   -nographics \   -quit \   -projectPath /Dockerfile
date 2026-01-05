FROM unityci/editor:2022.3.15f1-linux-il2cpp-3.1.0

WORKDIR /project

COPY . .

RUN unity-editor \
  -batchmode \
  -nographics \
  -quit \
  -projectPath /project \
  -buildTarget Linux64 \
  -executeMethod BuildScript.BuildLinux
