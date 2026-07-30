# 🎟️ Lottery Platform — פלטפורמת הגרלות Multi-Tenant בזמן אמת

פלטפורמת **Full-Stack** מקצה לקצה לניהול הגרלות עבור ארגונים וארגוני חסד — כל ארגון מקבל **אתר הרשמה עצמאי, ממותג ודינמי משלו** (`/join/<org-slug>`), שבו משתתפים רוכשים חבילות וכרטיסי הגרלה, ומנהלים שולטים על כל מחזור החיים העסקי: קטגוריות, מתנות, תורמים, הגרלה, זוכים והכנסות — הכול מתוך קוד בסיס אחד, משותף ומאובטח.

הפרויקט בנוי כשתי חזיתות עצמאיות שמדברות ביניהן דרך REST API מאובטח:

| שכבה | טכנולוגיה | תיקייה |
|---|---|---|
| **Server / API** | ASP.NET Core 8 · Entity Framework Core 9 · SQL Server | [`LotteryApi/`](LotteryApi/) |
| **Client / Web App** | Angular 20 (Standalone + Signals) · PrimeNG 21 | [`AngularProject/`](AngularProject/) |

---

## ✨ למה הפרויקט הזה מרשים

### 🎨 White-Label engine — מיתוג דינמי בזמן אמת, בלי בנייה מחדש
זו התכונה המרכזית של המערכת: **אותו אתר בדיוק משרת עשרות ארגונים שונים, וכל אחד נראה כאילו נבנה במיוחד בשבילו.**

ברגע שנטען נתיב כמו `/join/united-hatzalah` או `/join/mda-digital`, ה-Client מזהה את ה-`slug` מה-URL, ו-`OrgService` (בנוי על **Angular Signals**) מזריק את זהות המותג של הארגון — צבע ראשי, צבע משני, צבע הדגשה, לוגו — ישירות לתוך **CSS Custom Properties** של המסמך (`--primary-color`, `--secondary-color`, `--accent-color`...).

התוצאה: כל האתר — כפתורים, כרטיסים, כותרות, טפסים — **משתנה מיתוג באופן ריאקטיבי ומיידי**, ללא Reload, ללא build נפרד לכל לקוח, וללא כפילות קוד. תשעה ארגונים שונים, תשע חוויות מיתוג שונות, קובץ CSS אחד.

### 🏢 ארכיטקטורת Multi-Tenant אמיתית
כל ישות עסקית — חבילות, מתנות, תורמים, עגלות, הזמנות, הגרלות — משויכת לארגון (`Organization`) ברמת המודל, כך שכמה ארגונים פועלים **במקביל, על אותו שרת ומסד נתונים**, כל אחד עם המידע, המשתמשים והדוחות המבודדים שלו.

### 🔐 אבטחה ברמת Production
- הזדהות מבוססת **JWT Bearer** עם Issuer/Audience validation מלא ותפוגה מוגדרת.
- שני תפקידי הרשאה (`Manager` / `Participant`) שמפרידים בין ניהול לצריכה.
- סודות אפליקציה (כגון מפתח החתימה של ה-JWT) מנוהלים דרך **.NET User Secrets / משתני סביבה** ולא נשמרים בקוד המקור.
- שכבת **Exception Middleware** גלובלית ולוגים מובנים (Serilog, JSON מובנה + rolling files) לכל בקשה בשרת.

### 🛍️ פלואו עסקי מלא, מקצה לקצה
לא עוד CRUD — מדובר בזרימת מסחר אמיתית: **עיון בחבילות/מתנות → עגלת קניות → Checkout → הזמנה → הגרלה → דוח זוכים → דוח הכנסות**, עם ניהול תורמים וקטגוריות ברקע.

### 🧱 ארכיטקטורה נקייה בשני הצדדים
- **בשרת**: הפרדה מלאה בין Controllers → Services → Repositories → DTOs → Models, עם ממשקים (Interfaces) לכל שכבה — קל להרחיב, קל לבדוק.
- **בלקוח**: Angular 20 **Standalone Components** + **Signals** לניהול מצב ריאקטיבי מודרני, ללא NgModules מיושנים, עם ספריית עיצוב עשירה (PrimeNG + PrimeFlex).

---

## 🖥️ צד שרת (Server) — `LotteryApi/`

### טכנולוגיות
- **ASP.NET Core 8** Web API
- **Entity Framework Core 9** מול SQL Server (Code-First + Migrations)
- **JWT Authentication** (`Microsoft.AspNetCore.Authentication.JwtBearer`)
- **Serilog** — Console + קבצים מתגלגלים יומית
- **Swagger / Swashbuckle** — תיעוד API אינטראקטיבי חי

### מבנה התיקיות
```
LotteryApi/LotteryApi/
├── Controllers/     # נקודות קצה REST
├── Services/        # לוגיקה עסקית
├── Repositories/    # גישה לנתונים מול EF Core
├── Models/          # ישויות מסד הנתונים
├── Dtos/            # חוזי תקשורת בין Client ל-Server
├── Enums/           # CategoryEnum, UserRoleEnum, CardPriceEnum
├── Data/            # DbContext + Factory
├── Migrations/      # היסטוריית סכמת מסד הנתונים
├── Middleware/       # טיפול גלובלי בחריגות
└── Program.cs        # Composition Root והגדרת ה-Pipeline
```

### מודל הנתונים המרכזי
`Organization` (זהות ומיתוג הארגון) · `UserModel` · `CategoryModel` · `PackageModel` · `GiftModel` · `DonorModel` · `ShoppingCartModel` / `PackageInCartModel` / `GiftInCartModel` · `OrderModel` / `PackageInOrderModel` / `GiftInOrderModel`

### בקרי API
`AuthController` (הרשמה/התחברות) · `UserController` · `OrganizationsController` · `CategoryController` · `PackageController` · `GiftController` · `DonorController` · `ShoppingCartController` · `PackageInCartController` · `GiftInCartController` · `OrsersController` (הזמנות) · `PackageInOrderController` · `GiftInOrderController`

### הרצה מקומית
```bash
cd LotteryApi/LotteryApi

# הגדרת מפתח JWT מקומי (פעם אחת, לא נשמר בגיט)
dotnet user-secrets set "JwtSettings:SecretKey" "<ערך-אקראי-חזק-משלך>"

# יצירת/עדכון מסד הנתונים
dotnet ef database update

# הרצה
dotnet run
```
השרת עולה על `http://localhost:5052` (וב-HTTPS על `https://localhost:7211`), ותיעוד Swagger זמין תחת `/swagger`.

---

## 🌐 צד לקוח (Client) — `AngularProject/`

### טכנולוגיות
- **Angular 20** — Standalone Components + Signals
- **PrimeNG 21** + **PrimeFlex** — ספריית UI עשירה ורספונסיבית
- **RxJS**

### מבנה עיקרי
```
AngularProject/src/
├── app/
│   ├── components/
│   │   ├── organization-home/   # דף הבית הדינמי לפי ארגון
│   │   ├── package/ gifts/       # קטלוג חבילות ומתנות
│   │   ├── shopping-cart/ check-out/  # עגלה ותשלום
│   │   ├── login/ register/      # הזדהות
│   │   ├── manager/ manager-*/    # קונסולת ניהול (קטגוריות, חבילות, מתנות, תורמים)
│   │   ├── lottery/                # מנוע ביצוע ההגרלה
│   │   ├── report-winner/ revenue-report/  # דוחות
│   │   ├── orders/ header/ video/
│   ├── app.routes.ts
│   └── app.config.ts
├── models/    # טיפוסי TypeScript תואמי-DTO
└── services/  # OrgService, AuthService, ושירותי HttpClient לכל ישות
```

### המנוע הדינמי — `OrgService`
שירות מבוסס **Signals** ששומר את הארגון הפעיל (`currentOrg`) וממפה אותו בזמן אמת ל-CSS Custom Properties על ה-`<html>` root. כל קומפוננטה בעולם צורכת את אותם משתנים — כך שהוספת ארגון חדש עם מיתוג משלו היא שינוי נתונים בלבד, לא שינוי קוד.

### ניתוב לפי ארגון
```
join/:orgSlug                      → דף הבית (חבילות/מתנות)
join/:orgSlug/login | /register    → הזדהות
join/:orgSlug/checkout | /orders   → רכישה והזמנות
join/:orgSlug/manager*             → קונסולת ניהול מלאה
join/:orgSlug/report-winner        → דוח זוכים
join/:orgSlug/revenue-report       → דוח הכנסות
```

### הרצה מקומית
```bash
cd AngularProject
npm install
npm start   # http://localhost:4200
```

פקודות נוספות:
```bash
npm run build   # בנייה לפרודקשן
npm run watch   # בנייה עם צפייה בשינויים
npm test        # הרצת בדיקות Karma/Jasmine
```

---

## 🚀 הרצת המערכת המלאה

```bash
# טרמינל 1 — API
cd LotteryApi/LotteryApi
dotnet run

# טרמינל 2 — Client
cd AngularProject
npm install
npm start
```

לאחר מכן היכנסו ל-`http://localhost:4200/join/<org-slug>` — למשל `united-hatzalah`, `ezer-mizion`, `mda-digital` — וצפו באתר משנה את כל זהותו הוויזואלית בזמן אמת, לפי הארגון שנטען.

---

## דרישות מוקדמות
- .NET 8 SDK
- Node.js + npm (ו-Angular CLI, אופציונלי)
- SQL Server (מקומי / Express)
