import React, { useState, useContext } from 'react';
import { useNavigate, Link } from 'react-router-dom';
import { AuthContext } from '../context/AuthContext';
import { userService } from '../services/userService';
import '../styles/forms.css';

const LoginPage = () => {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [errors, setErrors] = useState([]);
  const { login } = useContext(AuthContext);
  const navigate = useNavigate();

  const handleSubmit = async (e) => {
    e.preventDefault();
    setErrors([]);
    try {
      const user = await userService.login(email, password);
      login(user);
      navigate('/tasks');
    } catch (err) {
      if (err.errors) {
        setErrors(err.errors);
      } else {
        setErrors(['Error al iniciar sesión']);
      }
    }
  };

  return (
    <div className="page-container">
      <div className="form-container">
        <h2 className="form-title">Iniciar Sesión</h2>
        
        {errors.length > 0 && (
          <div className="error-message">
            {errors.map((error, idx) => (
              <div key={idx}>{error}</div>
            ))}
          </div>
        )}

        <form onSubmit={handleSubmit}>
          <div className="form-group">
            <label className="form-label">Email</label>
            <input
              type="email"
              className="form-input"
              value={email}
              onChange={(e) => setEmail(e.target.value)}
              required
            />
          </div>

          <div className="form-group">
            <label className="form-label">Contraseña</label>
            <input
              type="password"
              className="form-input"
              value={password}
              onChange={(e) => setPassword(e.target.value)}
              required
            />
          </div>

          <button type="submit" className="btn-primary form-button">
            Ingresar
          </button>
        </form>

        <div className="form-footer">
          ¿No tienes cuenta? <Link to="/register">Regístrate aquí</Link>
        </div>
      </div>
    </div>
  );
};

export default LoginPage;
