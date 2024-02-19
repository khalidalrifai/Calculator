# Simple Calculator

## Overview

This WPF application provides a user-friendly interface for basic arithmetic calculations, including addition, subtraction, multiplication, and division. It's designed for ease of use with both mouse and keyboard inputs, featuring a clean, intuitive layout.


## Features

### Basic Arithmetic
- **Operations:** Perform addition, subtraction, multiplication, and division with ease.
- **Real-time Calculation Display:** See your calculations as you perform them, providing immediate feedback.
- **Clear Button:** Easily reset your current calculation with a single click.

### Advanced Functionalities
- **Memory Operations:** Includes MC (Memory Clear), MR (Memory Recall), MS (Memory Store), M+ (Memory Add), and M- (Memory Subtract) for efficient calculation workflows.
- **Reciprocal Calculation:** Quickly calculate the reciprocal of any number, enhancing the calculator's versatility.
- **Error Handling:** Advanced error management for scenarios like division by zero, ensuring a smooth user experience.

### Keyboard Support
Fully compatible with keyboard input, allowing for quicker data entry and operation. This feature is particularly beneficial for users accustomed to traditional calculator use.


## User Interface

The application boasts a straightforward and responsive interface, designed with user accessibility in mind. Key features include:
- **Real-time Calculation Display:** Immediate feedback is provided as you type and perform operations, enhancing usability.
- **Clear History:** Offers the option to clear the ongoing calculation history, allowing for a fresh start at any time.


## Prerequisites

- Visual Studio (or any compatible C# IDE) with support for WPF projects.
- .NET Framework, version 4.7.2 or above, to ensure full compatibility.


## Installation

1. Clone the repository: [https://github.com/khalidalrifai/Calculator.git](https://github.com/khalidalrifai/Calculator.git)
2. Open the solution file (`SimpleCalculator.sln`) in Visual Studio.
3. Build the solution to restore the NuGet packages and compile the code.
4. Run the application by pressing `F5` or clicking on the start button in Visual Studio.
5. Start calculating!


## Architecture

- **UI Layer:** Crafted using XAML to ensure a responsive and adaptable user interface.
- **Logic Layer:** The operational logic is encapsulated within MainWindow.xaml.cs.
- **SimpleMath Class:** A dedicated class for handling core arithmetic operations, ensuring modularity and ease of maintenance.


## Getting Started for Coding

For developers interested in contributing or exploring the code:

1. Follow the installation steps to set up the project environment.
2. Explore the `MainWindow.xaml` and `MainWindow.xaml.cs` files to understand the UI and logic implementation.
3. The `SimpleMath` class can be extended to include additional mathematical operations.


## Screenshots

1. Initial State:
![Main UI](/images/Simple_Calculator_Initial_State.png "Initial State")

2. Processing State:
![Main UI After Processing](/images/Simple_Calculator_Process_State.png "Processing State")


## Contributing

Contributions are highly encouraged! Whether it's a feature enhancement, bug fix, or documentation improvement, please follow these steps to contribute:

1. Fork the repository.
2. Create a new branch (`git checkout -b feature/your-feature-name`).
3. Make your changes.
4. Commit your changes (`git commit -am 'Add some feature'`).
5. Push to the branch (`git push origin feature/your-feature-name`).
6. Push the branch and submit a pull request for review.


## License

This project is open-sourced under the MIT License. For more details, see the [LICENSE](https://docs.gitlab.com/ee/development/licensing.html) file.


## Acknowledgments

- Thanks to [Microsoft](https://docs.microsoft.com/en-us/dotnet/framework/wpf/getting-started/walkthrough-my-first-wpf-desktop-application) for the WPF documentation.
