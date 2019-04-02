import React, { Component, Fragment } from 'react';
import { Row, Col, Button, Table, Well, FormGroup, ControlLabel, FormControl } from 'react-bootstrap';
import axios from 'axios';


class App extends Component {
  constructor(props) {
    super(props)

    this.state = {
      nameCity: 'fortaleza',
      cities: [],
      city: {
        previsao: []
      }
    }
  }

  /**
 * Formatar a data
 */
  formatDate = (dateTime) => {
    if (dateTime) {
      const date = new Date(dateTime);
      let day = date.getDate();
      let month = date.getMonth() + 1;
      month = month < 10 ? `0${month}` : month;
      day = day < 10 ? `0${day}` : day;
      return `${day}`.concat('/', month, '/', date.getFullYear());
    }
    return '-';
  }

  getCities = (nameCity) => {
    axios.get(`https://localhost:44306/api/CPTEC/GetByCityName/${nameCity}`)
      .then((response) => {
        let newState = { ...this.state };
        newState.cities.splice(0, newState.cities.length);
        newState.cities.push(...response.data);

        this.setState(newState);
      })
      .catch((error) => {
        // handle error
        console.log(error);
      })
      .then(() => {
        // always executed
      });
  }

  getPreview = (idCity) => {
    axios.get(`https://localhost:44306/api/CPTEC/GetByCityId/${idCity}`)
      .then((response) => {
        this.setState({ city: response.data });
      })
      .catch((error) => {
        // handle error
        console.log(error);
      })
      .then(() => {
        // always executed
      });
  }

  render() {
    let Nome = 'Fortaleza';
    let Uf = 'CE';
    return (
      <div id="main" className="container">
        <Row>
          <Col>
            <FormGroup controlId="Cidade">
                <ControlLabel>Cidade</ControlLabel>
                <FormControl value={this.state.nameCity} onChange={(e) => this.setState({ nameCity: e.target.value })} />
            </FormGroup>
            <FormGroup controlId="Cidade">
            <Button onClick={() => this.getCities(this.state.nameCity)}>Buscar</Button>
            </FormGroup>
          </Col>
        </Row>
        {this.state.cities.length ?
        <Row>
          <Table>
            <thead>
              <tr>
                <th>Id</th>
                <th>Nome</th>
                <th>Uf</th>
                <th></th>
              </tr>
            </thead>
            <tbody>
              {this.state.cities.map((item, i) =>
                <tr key={i}>
                  <td>{item.id}</td>
                  <td>{item.nome}</td>
                  <td>{item.uf}</td>
                  <td><Button onClick={() => this.getPreview(item.id)}>Previsão</Button></td>
                </tr>
              )}
            </tbody>
          </Table>
        </Row> : null}
        {this.state.city.previsao.length ?
        <Row>
          <Well>
            <p><strong>Cidade: </strong> {this.state.city.nome} - <strong>UF: </strong> {this.state.city.uf} - <strong>Data Consulta: </strong>{this.formatDate(new Date())}</p>
          </Well>
          <Table>
            <thead>
              <tr>
                <th>Data</th>
                <th>Tempo</th>
                <th>Máxima</th>
                <th>Mínima</th>
              </tr>
            </thead>
            <tbody>
              {this.state.city.previsao.map((item, i) =>
                <tr key={i}>
                  <td>{this.formatDate(item.dia)}</td>
                  <td>{item.tempo}</td>
                  <td>{item.minima}</td>
                  <td>{item.maxima}</td>
                </tr>
              )}
            </tbody>
          </Table>
        </Row> : null }
      </div>
    );
  }
}

export default App;
