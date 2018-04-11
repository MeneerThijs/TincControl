# TincControl

This project allows you to start and stop the Tinc VPN service from a Windows Notification Area icon. The color of the icon reflects the status of the service.

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes. See deployment for notes on how to deploy the project on a live system.

### Prerequisites

This project assumes you have Tinc VPN installed and configured as a Windows service. See https://www.tinc-vpn.org/examples/windows-install/ for instructions.

Because the NETNAME for your Tinc service is hardcoded in the **Properties/Resources.resx** file, you need Visual Studio (2017) to compile the project to a working binary.

The executable needs permission to manage Services. This can be achieved by running the executable as Administrator.

### Installing

Steps to create the binary:

```
Open the project in visual studio
Edit TincControl/Properties/Resources.resx and change __TINC_SERVICE_NAME_HERE__ to the your Tinc service name: tinc.<network_name>
Build the project
Start the executable with administrative provileges
```

The TincControl icon will now appear in the Notification Area.

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details

