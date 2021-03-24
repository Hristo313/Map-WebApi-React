import { useEffect, useState } from 'react';
import { Formik, Form, Field } from 'formik';
import axios from 'axios';

export const LogisticCenters = () => {
  const [towns, setTowns] = useState();
  const [selectedTowns, setSelectedTowns] = useState([]);
  const [logisticCenter, setLogisitcCenter] = useState();

  useEffect(() => {
    axios.get('https://localhost:44321/api/Towns').then(response => {setTowns(response.data)});
  }, [setTowns]);

  const handleSubmit = (values, { resetForm }) => {
    axios.post('https://localhost:44321/api/LogisticCenters', { Towns: selectedTowns , LogisticCenter: logisticCenter})
      .then((response) => {setLogisitcCenter(response.data)});
    resetForm({});
  }

  const addTown = (id, name) => {
    if (selectedTowns.filter(st => st.Name === name).length === 0) {
      setSelectedTowns(st => [...st, { Name: name }]);
    }
  }

  return (
    <div>
       <h1>Logistic Centers</h1>
       <Formik initialValues={{ towns: [] }} onSubmit={handleSubmit}>
        <Form>
          <div>
            <Field as="select" name="towns" multiple>
              <option>Select Towns</option>
             {towns && towns.map(t => <option key={t.id} onClick={() => addTown(t.id, t.name)}>{t.name}</option>)}
            </Field>
          </div>
          <button type="submit">Find Logistic Center</button>
        </Form>
       </Formik>
       <h2>Region</h2>
       <ul>
          {selectedTowns.map((st, index) => <li key={index}>{ st.Name }</li>)}
       </ul>
       <h2>Current Logistic Center</h2>
       <ul>
          {logisticCenter && <li>{logisticCenter.name}</li>}
       </ul>
    </div>
  );
}