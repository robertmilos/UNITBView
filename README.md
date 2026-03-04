# UNITBView

This branch is maintained by **Macovei Tudor**.

## 🚀 Overview
This branch serves as a proof-of-concept using a basic 3D template to demonstrate the functionality and performance of the target engine.

## 🛠 Technical Specifications
| Category | Detail |
| :--- | :--- |
| **Development Environment** | Unity |
| **Engine Version** | Unity 6.3 LTS (`6000.3.10f1`) |
| **Graphics API** | WebGL 2.0 |
| **Hardware Context** | Verified on:<br> Ryzen 5 7600X & Radeon RX 7800XT <br> Ryzen 5 7600X & Nvidia 4060Ti <br> i5-8265H & UHD 620 <br> Apple M2 |

## 🌐 Deployment & Networking
The build is served via a local backend and made accessible through the following configuration:
* **Web Server:** `python3` (HTTP server module) bound to a local port.
* **Connectivity:** Configured via manual Port Forwarding for external access.
* **Server Host:** MSI GE63 Raider (Fedora Linux).

## 🧪 Compatibility Testing
The application has been verified to run flawlessly on the following browsers:
* **Brave** (Chromium-based)
* **Firefox** (Gecko-based)
