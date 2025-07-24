# SpectreSpy

## Overview
SpectreSpy is a Windows application designed to interact with the Spectre Network API. It retrieves and displays chain data in a modern dark-themed interface.

## Features
- Fetches network information, price data, health status, and market capitalization from the Spectre Network API.
- Displays data in a user-friendly interface with a dark modern color scheme.
- Implements MVVM (Model-View-ViewModel) architecture for better separation of concerns.

## Project Structure
```
spectrespy
├── src
│   ├── Api
│   │   └── SpectreApiClient.cs
│   ├── Models
│   │   └── SpectreDataModel.cs
│   ├── Views
│   │   ├── MainWindow.xaml
│   │   └── MainWindow.xaml.cs
│   ├── ViewModels
│   │   └── MainViewModel.cs
│   ├── Themes
│   │   └── DarkTheme.xaml
│   └── App.xaml
├── spectrespy.sln
└── README.md
```

## Setup Instructions
1. Clone the repository to your local machine.
2. Open the solution file `spectrespy.sln` in your preferred IDE.
3. Restore the NuGet packages if necessary.
4. Build the solution to ensure all dependencies are resolved.
5. Run the application.

## Usage
- Upon launching the application, the main window will display chain info retrieved from the Spectre Network API.
- The data is updated in real-time, providing users with the latest information.

## Spectre Network API
The application interacts with the Spectre Network API to fetch the following data:
- **Network Info**: General information about the network.
- **Price Info**: Current price data for various assets.
- **Health Info**: Status of the network's health.
- **Market Cap Info**: Market capitalization details.

## Contributing
Contributions are welcome! Please submit a pull request or open an issue for any enhancements or bug fixes.

## License
This project is licensed under the MIT License. See the LICENSE file for more details.
