# SpiceworksExtract
Super simple command line app to extract device data to csv for manual import

Works in conjunction with the .Net 4.5 Service Agent application.

Service agent is designed to work with a direct connection to your server but if it is not available, you need to manually add devices.
This will let you save to CSV to import.

Currently have Spiceworks agent files in here from debug but who knows if that is legal.

Install the agent, just placing a dot in all the mandatory fields (server address and key.
Copy the SpiceworksExtract.exe into the program files folder and run it.

Alternatively, install, copy all the files out elsewhere, then uninstall.

**Example run**

```
PS C:\Program Files (x86)\Spiceworks\Agent> .\SpiceworksExtract.exe --addtimestamp --out="C:\SpiceworksData\"
--addTimestamp  - Adds timestamp to output name
--out="XXX"     - Specify output directory, default is current directory

Outputting to C:\SpiceworksData\tankpc.20210410-013122.000.csv
```

**Added to normal install path**

![image](https://user-images.githubusercontent.com/73286843/114204735-e0c6b980-999c-11eb-847b-754bdee572a1.png)

**Copied out to another location**

![image](https://user-images.githubusercontent.com/73286843/114205023-38fdbb80-999d-11eb-8579-57b19e43b8b9.png)
