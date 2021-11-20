# CalypsoAPI

API used to access state of Zeiss measuring machines using Calypso software.  
*Tested with version: 6.4.24 (must have CalypsoMonitor installed)*

#### Current Features:
- Read current state of CMM: Running, Paused, Stopped, Finished, Exception
- Read measurement informations: name of plan, path, part number, operator, (chr) results file path, ...

#### Planned Features:
- Read results of measurement
- Read current stylus and probe
- Remote start, pause or stop CMM
- Access more informations of the current measurement plan (speed, basesystem, pdf-export enabled, ...)
- Release NuGet
