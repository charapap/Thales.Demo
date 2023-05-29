# Thales.Demo

This is an application that facilitates role and person management.

It supports communication with other applications using TCP/IP connections.

**Configuration for the TCP/IP connection:**

To configure the TCP/IP connection settings, follow these steps:
1. Open the Thales.Demo.exe.config file.
2. Locate the appSettings section.
3. Set the desired port number that the client will listen to by updating the value of the "CurrentPort" key.
4. Add the IP addresses and ports of the other applications to be connected to the client in the "ConnectionIps" key.

Example configuration::
```
<add key="CurrentPort" value="3000"/>
<add key="ConnectionIps" value="192.168.1.170:3001,192.168.1.170:3002"/>
```

**Note**: Ensure that the different clients are placed in separate folders on the same machine to have different configuration files for testing purposes.
