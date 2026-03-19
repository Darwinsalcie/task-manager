import React, { useState, useEffect } from 'react';
import '../styles/forms.css';

const TaskForm = ({ onSubmit, onCancel, initialData }) => {
  const [formData, setFormData] = useState({
    title: '',
    description: '',
    start: '',
    end: '',
    isCompleted: false
  });

  const [errors, setErrors] = useState([]);

  useEffect(() => {
    if (initialData) {
      setFormData({
        title: initialData.title || '',
        description: initialData.description || '',
        start: initialData.start ? initialData.start.split('T')[0] : '',
        end: initialData.end ? initialData.end.split('T')[0] : '',
        isCompleted: initialData.isCompleted || false
      });
    }
  }, [initialData]);

  const handleChange = (e) => {
    const { name, value, type, checked } = e.target;
    setFormData(prev => ({
      ...prev,
      [name]: type === 'checkbox' ? checked : value
    }));
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    setErrors([]);
    try {
      await onSubmit(formData);
    } catch (err) {
      if (err.errors) {
        setErrors(err.errors);
      } else {
        setErrors(['Error al guardar la tarea']);
      }
    }
  };

  return (
    <div className="modal-overlay">
      <div className="modal-content">
        <div className="modal-header">
          <h2>{initialData ? 'Editar Tarea' : 'Nueva Tarea'}</h2>
          <button className="close-btn" onClick={onCancel}>&times;</button>
        </div>

        {errors.length > 0 && (
          <div className="error-message">
            {errors.map((error, idx) => (
              <div key={idx}>{error}</div>
            ))}
          </div>
        )}

        <form onSubmit={handleSubmit}>
          <div className="form-group">
            <label className="form-label">Título</label>
            <input
              type="text"
              name="title"
              className="form-input"
              value={formData.title}
              onChange={handleChange}
              required
            />
          </div>

          <div className="form-group">
            <label className="form-label">Descripción</label>
            <textarea
              name="description"
              className="form-input"
              rows="3"
              value={formData.description}
              onChange={handleChange}
            ></textarea>
          </div>

          <div className="form-group">
            <label className="form-label">Fecha de Inicio</label>
            <input
              type="date"
              name="start"
              className="form-input"
              value={formData.start}
              onChange={handleChange}
              required
            />
          </div>

          <div className="form-group">
            <label className="form-label">Fecha de Fin</label>
            <input
              type="date"
              name="end"
              className="form-input"
              value={formData.end}
              onChange={handleChange}
              required
            />
          </div>

          {initialData && (
            <div className="form-checkbox-group">
              <input
                type="checkbox"
                name="isCompleted"
                id="isCompleted"
                className="form-checkbox"
                checked={formData.isCompleted}
                onChange={handleChange}
              />
              <label htmlFor="isCompleted">Marcar como completada</label>
            </div>
          )}

          <div className="modal-actions">
            <button type="button" className="btn-secondary" onClick={onCancel}>
              Cancelar
            </button>
            <button type="submit" className="btn-primary">
              Guardar
            </button>
          </div>
        </form>
      </div>
    </div>
  );
};

export default TaskForm;
