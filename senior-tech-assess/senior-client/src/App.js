import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import './App.css';
import AllPatient from './components/AllPatients.js';
import LoginPage from './components/LoginPage.js';

function App() {
    return (
        <Router>
            <div className="App">
                <Routes>
                    <Route path="/login" element={<LoginPage />} />
                    <Route path="/home" element={<AllPatient />} />
                    <Route path="*" element={<LoginPage />} />
                </Routes>
            </div>
        </Router>
    );
}

export default App;
