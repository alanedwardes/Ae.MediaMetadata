FROM --platform=linux/arm64/v8 mcr.microsoft.com/dotnet/runtime:6.0

ADD build/output /opt/mediametadata

VOLUME ["/data"]

WORKDIR /data

ENTRYPOINT ["/opt/mediametadata/Ae.MediaMetadata.Console"]
