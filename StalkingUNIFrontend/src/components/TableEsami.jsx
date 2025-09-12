import { useState } from 'react';
import Box from '@mui/material/Box';
import Collapse from '@mui/material/Collapse';
import IconButton from '@mui/material/IconButton';
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import Typography from '@mui/material/Typography';
import Paper from '@mui/material/Paper';
import KeyboardArrowDownIcon from '@mui/icons-material/KeyboardArrowDown';
import KeyboardArrowUpIcon from '@mui/icons-material/KeyboardArrowUp';


function Row({ row, nomeEsame }) {
  const [open, setOpen] = useState(false);

  return (
    <>
      <TableRow sx={{ '& > *': { borderBottom: 'unset' } }}>
        <TableCell>
          {
            row &&
            <IconButton aria-label="expand row" size="small" onClick={() => setOpen(!open)}>
              {open ? <KeyboardArrowUpIcon /> : <KeyboardArrowDownIcon />}
            </IconButton>
          }
        </TableCell>
        <TableCell component="th" scope="row">{nomeEsame}</TableCell>
        <TableCell align="right">{row ? adaptVoto(row[0].Voto) : "-"}</TableCell>
      </TableRow>
      <TableRow>
        <TableCell style={{ paddingBottom: 0, paddingTop: 0 }} colSpan={6}>
          <Collapse in={open} timeout="auto" unmountOnExit>
            <Box sx={{ margin: 1 }}>
              <Typography component="div">Data: {row ? adaptData(row[0].Data) : "-"}</Typography>
              <Typography component="div">Tipo: {row ? adaptTipo(row[0].TipoEsame) : "-"}</Typography>

              {(row && row[1]) && <Typography variant="h6" component="div"> Precedenti appelli sostenuti: </Typography>}
              {
                (row && row[1]) &&
                <Table size="small" aria-label="purchases">
                  <TableHead>
                    <TableRow>
                      <TableCell>Data</TableCell>
                      <TableCell>Tipologia</TableCell>
                      <TableCell align="right">Voto</TableCell>
                    </TableRow>
                  </TableHead>
                  <TableBody>
                    {row.map((elem, index) => (
                      index != 0 &&
                      <TableRow key={elem.Data}>
                        <TableCell component="th" scope="row">{adaptData(elem.Data)}</TableCell>
                        <TableCell>{adaptTipo(elem.TipoEsame)}</TableCell>
                        <TableCell align="right">{adaptVoto(elem.Voto)}</TableCell>
                      </TableRow>
                    ))}
                  </TableBody>
                </Table>
              }
            </Box>
          </Collapse>
        </TableCell>
      </TableRow>
    </>
  );
}


export default function CollapsibleTable({ data }) {
  return (
    <TableContainer component={Paper}>
      <Table aria-label="collapsible table">
        <TableHead>
          <TableRow>
            <TableCell />
            <TableCell>Nome esame</TableCell>
            <TableCell align="right">Voto</TableCell>
          </TableRow>
        </TableHead>
        <TableBody>
          {Object.entries(data).map(([key, value], index) => (
            <Row key={index} nomeEsame={key} row={value} />
          ))}
        </TableBody>
      </Table>
    </TableContainer>
  );
}

function adaptVoto(voto) {
  let res = ""

  switch (voto) {
    case 31:
      res = "30 E LODE"
      break
    case 0:
      res = "Insufficiente"
      break
    default:
      res = `${voto}`
      break
  }

  return res
}

function adaptData(date) {
  let d = new Date(date)
  return `${d.getDate()}/${d.getMonth() + 1}/${d.getFullYear()}`
}

function adaptTipo(type) {
  let res = ""

  switch (type) {
    case "P":
      res = "Parziale"
      break
    case "C":
      res = "Completo"
      break
    case "R":
      res = "Recupero"
      break
  }

  return res
}
