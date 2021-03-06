import { useEffect, useState } from 'react';
import { Formik, Form, Field } from 'formik';
import axios from 'axios';
import classes from './Towns.module.css';

export const Towns = () => {
  const [towns, setTowns] = useState();

  useEffect(() => {
    axios.get('https://localhost:44321/api/Towns')
      .then(response => {
        setTowns(response.data);
      });
  }, [setTowns]);

  const handleSubmit = async (values, { resetForm }) => {
    if (towns.filter(t => t.name === values.Name).length === 0) {
      await axios.post('https://localhost:44321/api/Towns', { Name: values.Name });
      axios.get('https://localhost:44321/api/Towns')
      .then(response => {
        setTowns(response.data);
      });
      resetForm({});
    }
  }

  const handleDelete = async (id) => {
    await axios.delete(`https://localhost:44321/api/Towns/${id}`);
    axios.get('https://localhost:44321/api/Towns')
      .then(response => {
        setTowns(response.data);
      });
  }

  return (
   <div className={classes.Towns}>
      <h1>Towns</h1>
      <Formik initialValues={{ Name: '' }} onSubmit={handleSubmit}>
      <Form>
        <div>
          <label>Town Name</label>
          <Field name="Name" autoComplete="off" />
        </div>
        <button type="submit">Add Town</button>
      </Form>
      </Formik>
      <ul>
        {towns && towns.map(t => {
          return <li key={t.id}>{t.name} <button onClick={() => handleDelete(t.id)}>del</button></li>;
        })}
      </ul>
   </div>
  )
}
