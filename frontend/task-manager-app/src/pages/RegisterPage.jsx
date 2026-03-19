import React, { useState } from 'react';
import { useNavigate, Link } from 'react-router-dom';
import { userService } from '../services/userService';
import '../styles/forms.css';

const RegisterPage = () => {
  const [name, setName] = useState('');
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [errors, setErrors] = useState([]);
  const navigate = useNavigate();

  const handleSubmit = async (e) => {
    e.preventDefault();
    setErrors([]);
    try {
      await userService.create({
        name,
        email,
        password,
        userRole: 1 // Default to User
      });
      navigate('/login');
    } catch (err) {
      if (err.errors) {
        setErrors(err.errors);
      } else {
        setErrors(['Error al registrar usuario']);
      }
    }
  };

  return (
    <div className="page-container">
      <div className="form-container">
        <h2 className="form-title">Registro</h2>
        
        {errors.length > 0 && (
          <div className="error-message">
            {errors.map((error, idx) => (
              <div key={idx}>{error}</div>
            ))}
          </div>
        )}

        <form onSubmit={handleSubmit}>
          <div className="form-group">
            <label className="form-label">Nombre</label>
            <input
              type="text"
              className="form-input"
              value={name}
              onChange={(e) => setName(e.target.value)}
              required
            />
          </div>

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
              minLength="6"
            />
          </div>

          <button type="submit" className="btn-primary form-button">
            Registrarse
          </button>
        </form>

        <div className="form-footer">
          ¿Ya tienes cuenta? <Link to="/login">Inicia Sesión</Link>
        </div>
      </div>
    </div>
  );
};

export default RegisterPage;
