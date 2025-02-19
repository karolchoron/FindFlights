# ✈️ FindFlights - Frontend
 
[![Website](https://img.shields.io/badge/Live%20Demo-Available-green?style=for-the-badge)](https://findflights.onrender.com)

FlightTrack to aplikacja do śledzenia statusów lotów w czasie rzeczywistym. 🛫

🔗 **Uruchom aplikację:**  
➡ [https://findflights.onrender.com](https://findflights.onrender.com)  

---

## 🚀 **Technologie**
- ✅ Blazor WebAssembly 
- ✅ .NET
- ✅ C#  
- ✅ HTML, CSS, JavaScript
- ✅ RESTfull API
- ✅ Docker - deploy & DevOps na render.com

---

## 📥 **Instalacja (Lokalnie)**
0. **Pamiętaj aby przygotować również aplikację backendową!**
<br> ➡ [https://github.com/karolchoron/FindFlightsAPIServer](https://github.com/karolchoron/FindFlightsAPIServer)  

1. **Klonuj repozytorium**  
2. **Skonfiguruj Program.cs**
Połącznie z backendem:
```
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5240/") });
Port dla:
HTTP: 5240
HTTPS: 7220
```

3. **Uruchom aplikację**

### 📌 Przykładowe numery prawdziwych lotów:
- LO267
- MM68
- 7C2108
- CX524
- QR5844

---

🤝 **Kontakt** 
Masz pytania?

Skontaktuj się przez  
[![LinkedIn](https://img.shields.io/badge/LinkedIn-Connect-blue?logo=linkedin&style=for-the-badge)](https://www.linkedin.com/in/karol-choron/)

---
