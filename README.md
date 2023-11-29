# Lumia Firmware Downloader

Tool to fetch firmware &amp; data from Microsoft Lumia Software Repository Server.

This is a .NET 4.5 & VS2013 adaptation based on a fork of Gus's [SoReFetch](https://hub.nuaa.cf/gus33000/SoReFetch). Added an option suppress output.

## Help

```
Lumia Firmware Downloader
Adapted for .NET 4.5 by Picsell Dois (PC-DOS)

Based on:
Software Repository (SoRe) Fetch Utility
Copyright 2021 (c) Gustave Monce

This software uses code from the following open source projects released under the MIT license:

WPinternals - Copyright (c) 2018, Rene Lergner - wpinternals.net - @Heathcliff74xda

Please see 3RDPARTY.txt for more information

  -c, --product-code         Product Code.

  -t, --product-type         Required. Product Type.

  -o, --operator-code        Operator Code.

  -r, --revision             Revision (useful for getting older package versions).

  -f, --firmware-revision    Phone Firmware Revision (useful only for changing what Test package you get for
                             ENOSW/MMOS).

  -v, --verbose-output       (Default: true) Enable verbose output.
  
  --help                     Display this help screen.

  --version                  Display version information.

```

Example:

```LumiaFirmwareDownloader -c 059X4D5 -f 01078.00053.16236.35035 -o ATT-US -t RM-1104```
```LumiaFirmwareDownloader -t RM-1050 -v off```
