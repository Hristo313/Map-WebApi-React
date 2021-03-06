import { useEffect, useState } from 'react'
import { Formik, Form, Field } from 'formik';
import axios from 'axios';

export const Routes = () => {
  const [towns, setTowns] = useState();
  const [routes, setRoutes] = useState();

  useEffect(() => {
    axios.get('https://localhost:44321/api/Towns')
      .then(response => {
        setTowns(response.data);
      });
    axios.get('https://localhost:44321/api/Routes')
    .then(response => setRoutes(response.data));
  }, [setTowns, setRoutes]);

  const handleSubmit = async (values, { resetForm }) => {
    if (routes.filter(r => (r.start === values.Start && r.end === values.End) || (r.start === values.End && r.end === values.Start)).length !== 0) {
      return;
    }

    if (values.Start !== 'Select Start Point' && values.End !== 'Select Start Point') {
      await axios.post(`https://localhost:44321/api/Routes/${values.Start}/${values.End}/${Number(values.Length)}`);
      axios.get('https://localhost:44321/api/Routes').then(response => setRoutes(response.data));
      resetForm({});
    }
  }

  const handleDelete = async (id) => {
    await axios.delete(`https://localhost:44321/api/Routes/${id}`);
    axios.get('https://localhost:44321/api/Routes').then(response => setRoutes(response.data));
  } 

  return (
    <div>
      <Formik initialValues={{ Start: '', End: '', Length: '' }} onSubmit={handleSubmit}>
        {({ values }) => (
          <Form>
            <div>
              <label>From</label>
              <Field as="select" name="Start">
                <option>Select Start Point</option>
                {towns && towns.filter(t => t.name !== values.End).map(t => <option key={t.id}>{t.name}</option>)}
              </Field>
            </div>
            <div>
              <label>To</label>
              <Field as="select" name="End">
                <option>Select End Point</option>
                {towns && towns.filter(t => t.name !== values.Start).map(t => <option key={t.id}>{t.name}</option>)}
              </Field>
            </div>
            <div>
              <label>Length</label>
              <Field type="number" name="Length" min="1" />
            </div>
            <button type="submit">Add Route</button>
          </Form>
        )}
      </Formik>
      <ul>
        {routes && routes.map(r => <li key={r.id}>{r.start}-{r.end}: {r.length} <button onClick={() => handleDelete(r.id)}>del</button></li>)}
      </ul>
    </div>
  )
}