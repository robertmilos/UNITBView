import { initializeApp } from "https://www.gstatic.com/firebasejs/10.8.0/firebase-app.js";
import { 
    getAuth, 
    signInWithPopup, 
    GoogleAuthProvider, 
    signOut, 
    onAuthStateChanged,
    createUserWithEmailAndPassword,
    signInWithEmailAndPassword 
} from "https://www.gstatic.com/firebasejs/10.8.0/firebase-auth.js";

const firebaseConfig = {
  apiKey: "AIzaSyAMgmLpNZT15WcVeeXtOxyb872Vw8Hc6Do",
  authDomain: "unitbview.firebaseapp.com",
  projectId: "unitbview",
  storageBucket: "unitbview.firebasestorage.app",
  messagingSenderId: "697999890234",
  appId: "1:697999890234:web:e9155cf84f6a4bc297a203",
  measurementId: "G-RN0WJQE2NR"
};

const app = initializeApp(firebaseConfig);
const auth = getAuth(app);
const googleProvider = new GoogleAuthProvider();

const authContainer = document.getElementById('authContainer');
const userProfile = document.getElementById('userProfile');
const emailInput = document.getElementById('emailInput');
const passInput = document.getElementById('passInput');

// Creare cont nou
document.getElementById('emailSignupBtn').onclick = async () => {
    const email = emailInput.value;
    const pass = passInput.value;

    if (!email || !pass) {
        alert("Te rog completează atât emailul, cât și parola!");
        return;
    }

    if (!/[A-Z]/.test(pass)) {
        alert("Eroare: Parola trebuie să conțină cel puțin o literă mare (majusculă)!");
        return;
    }

    try {
        await createUserWithEmailAndPassword(auth, email, pass);
        
        emailInput.value = '';
        passInput.value = '';
        
        alert("Cont creat cu succes! Acum ești logat.");
    } catch (error) {
        console.error("Eroare internă:", error.code); 
        
        if (error.code === 'auth/email-already-in-use') {
            alert("Eroare: Acest email este deja înregistrat. Încearcă să te loghezi.");
        } else if (error.code === 'auth/weak-password') {
            alert("Eroare: Parola este prea slabă. Trebuie să aibă minim 6 caractere.");
        } else if (error.code === 'auth/invalid-email') {
            alert("Eroare: Adresa de email nu este validă.");
        } else {
            alert("A apărut o problemă la crearea contului. Te rog încearcă din nou.");
        }
    }
};

// Logare email existent
document.getElementById('emailLoginBtn').onclick = async () => {
    try {
        await signInWithEmailAndPassword(auth, emailInput.value, passInput.value);
    } catch (error) {
        console.error("Eroare internă:", error.code);
        
        if (error.code === 'auth/invalid-credential' || error.code === 'auth/user-not-found' || error.code === 'auth/wrong-password') {
            alert("Eroare: Email sau parolă incorectă.");
        } else if (error.code === 'auth/invalid-email') {
            alert("Eroare: Adresa de email nu este validă.");
        } else {
            alert("A apărut o problemă la logare. Te rog încearcă din nou.");
        }
    }
};

// --- LOGICĂ GOOGLE ---
document.getElementById('googleBtn').onclick = async () => {
    try {
        await signInWithPopup(auth, googleProvider);
    } catch (error) {
        console.error("Eroare internă Google:", error.code);
        
        if (error.code !== 'auth/popup-closed-by-user') {
            alert("Autentificarea cu Google a eșuat. Te rog încearcă din nou.");
        }
    }
};

// --- LOGOUT ---
document.getElementById('logoutBtn').onclick = () => {
    signOut(auth).catch(err => console.error("Eroare logout:", err));
};

onAuthStateChanged(auth, (user) => {
    if (user) {
        console.log("Te trimitem pe pagina nouă...");
    
        window.location.href = "pagina.html"; 
        
    } else {
        console.log("Utilizatorul nu este logat. Se afișează formularul.");
        
        document.querySelector('.login-box').classList.remove('hidden');
        
        const welcomeText = document.querySelector('.welcome-text');
        if (welcomeText) {
            welcomeText.classList.remove('hidden');
        }
    }
});
