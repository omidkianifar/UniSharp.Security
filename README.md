
# UniSharp.Security.Cryptography

A flexible and extensible cryptography library for Unity, supporting multiple encryption algorithms like DES and AES. This library allows developers to easily integrate encryption and decryption functionalities into Unity projects, with customizable options and Unity editor integration.

---

## **Features**

- **Supported Algorithms**:
  - DES (Data Encryption Standard)
  - AES (Advanced Encryption Standard)
    - AES-128
    - AES-192
    - AES-256

- **Unity Integration**:
  - Configurable via ScriptableObject (`CryptographySettings`).
  - Easy runtime configuration with an intuitive API.

- **Customizable**:
  - Use default keys or provide your own.
  - Supports extending for additional algorithms.

---

## **Installation**

1. Clone or download this repository:
   ```bash
   git clone https://github.com/omidkianifar/UniSharp.Security.git
   ```
2. Add the `UniSharp.Security.Cryptography` folder to your Unity project's `Assets` directory.

---

## **Getting Started**

### **Step 1: Configure Settings**
1. Create a `CryptographySettings` asset:
   - In Unity, right-click in the Project window.
   - Select **Create > UniSharp > Cryptography Settings**.
   - Configure your default algorithm and keys.

### **Step 2: Create and Use a Cryptographer**
Here’s an example of how to create a cryptographer and use it for encryption and decryption:

```csharp
using UniSharp.Security.Cryptography;

void Example()
{
    // Create an AES cryptographer
    var cryptographer = CryptoProvider.Create(options =>
    {
        options.Algorithm = AlgorithmType.AES128;
        options.Key1 = "YourAESKey123456"; // 16 bytes
        options.Key2 = "YourAESIV123456";  // 16 bytes
    });

    string plainText = "Hello, World!";
    string encrypted = cryptographer.Encrypt(plainText);
    string decrypted = cryptographer.Decrypt(encrypted);

    Debug.Log($"Plain Text: {plainText}");
    Debug.Log($"Encrypted: {encrypted}");
    Debug.Log($"Decrypted: {decrypted}");
}
```

---

## **APIs**

### **CryptoProvider**
Factory class for creating cryptographers.
```csharp
public static ICryptographer Create(Action<Options> setupAction = null);
```

- `setupAction`: (Optional) Configure algorithm type, keys, etc.

### **ICryptographer**
Interface for cryptographer implementations.
```csharp
string Encrypt(string plainText);
string Decrypt(string cipherText);
```

---

## **CryptographySettings**
ScriptableObject for configuring default settings.
- **General Settings**:
  - `Default Algorithm`: Set the default algorithm type.
- **DES Settings**:
  - Public/Private keys (64-bit).
- **AES Settings**:
  - Key and IV for AES-128, AES-192, and AES-256.

---

## **Extending the Library**

1. Add a new algorithm to the `AlgorithmType` enum.
2. Create a new cryptographer class inheriting from `BaseCryptographer`.
3. Add a case for your new algorithm in `CryptoProvider`.

---

## **Contributing**

Contributions are welcome! Feel free to submit issues or pull requests.

---

## **License**

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.
