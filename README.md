
# UniSharp.Security.Cryptography

A flexible and extensible cryptography and obfuscation library for Unity, supporting multiple encryption algorithms like DES and AES. This library allows developers to easily integrate encryption, decryption, and runtime value obfuscation functionalities into Unity projects, with customizable options and Unity editor integration.

---

## **Features**

- **Supported Algorithms**:
  - DES (Data Encryption Standard)
  - AES (Advanced Encryption Standard)
    - AES-128
    - AES-192
    - AES-256

- **Runtime Obfuscation**:
  - Protects sensitive values like integers, floats, doubles, and more using XOR-based obfuscation.
  - Includes built-in tampering detection mechanisms.
  - Seamlessly integrates with JSON serialization.

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

### **Obfuscated Types**
Runtime obfuscation is provided for sensitive data types like `int`, `float`, `double`, `long`, and `ulong`. These types use XOR-based obfuscation and include tamper detection.

#### Example:
```csharp
using UniSharp.Security.Obfuscation;

// Assigning and retrieving obfuscated values
ObfuscateInt secureInt = 42; // Automatically obfuscates the value
int actualValue = secureInt; // Deobfuscates to retrieve the original value

// Cheat detection
ObfuscateInt.OnCheetDetected += (value) => Debug.Log("Tampering detected!");
secureInt = 99; // If tampered, triggers the cheat detection event

// Serialization example
string json = JsonConvert.SerializeObject(secureInt); // Converts to a plain number
ObfuscateInt deserialized = JsonConvert.DeserializeObject<ObfuscateInt>(json);
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
