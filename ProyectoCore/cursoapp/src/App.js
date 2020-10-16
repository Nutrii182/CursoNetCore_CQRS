import React, { useEffect, useState } from 'react';
import theme from "./theme/theme";
import { Grid, Snackbar } from '@material-ui/core';
import Login from './components/Seguridad/Login';
import { BrowserRouter as Router, Switch, Route } from 'react-router-dom';
import RegistrarUsuario from './components/Seguridad/RegistrarUsuario';
import PerfilUsuario from './components/Seguridad/PerfilUsuario';
import { ThemeProvider as MuithemeProvider} from "@material-ui/core/styles";
import AppNavBar from './components/Navegacion/AppNavbar';
import { useStateValue } from './context/store';
import { usuarioActual } from './actions/UsuarioAction';

function App() {

  const [{sesionUsuario, openSnackbar}, dispatch] = useStateValue();
  const [startApp, setStartApp] = useState(false);
  
  useEffect(() => {
    if(!startApp){
      usuarioActual(dispatch).then(response => {
        setStartApp(true);
      }).catch(error => {
        setStartApp(true);
      });
    }
  },[startApp]);

  return startApp === false ? null : (
    <React.Fragment>
      <Snackbar
        anchorOrigin={{vertical:"bottom", horizontal:"center"}}
        open={openSnackbar ? openSnackbar.open : false}
        autoHideDuration={3000}
        ContentProps={{"aria-describedby" : "message-id"}}
        message = {
          <span id="message-id">{openSnackbar ? openSnackbar.message : ""}</span>
        }
        onClose = { () =>
          dispatch({
            type : "OPEN_SNACKBAR",
            openMessage : {
              open : false,
              message : ""
            }
          })
        }
      >
      </Snackbar>
      <Router>
        <MuithemeProvider MuithemeProvider theme={theme}>
        <AppNavBar/>
        <Grid container>
          <Switch>
            <Route exact path="/auth/login" component={Login}/>
            <Route exact path="/auth/registrar" component={RegistrarUsuario}/>
            <Route exact path="/auth/perfil" component={PerfilUsuario}/>
            <Route exact path="/" component={Login}/>
          </Switch>
        </Grid>
        </MuithemeProvider>
      </Router>
    </React.Fragment>
  );
}

export default App;
