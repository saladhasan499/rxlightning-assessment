import React, { useState } from 'react';
import {
    Button,
    Table,
    TableBody,
    TableCell,
    TableContainer,
    TableHead,
    TableRow,
    Paper,
    Accordion,
    AccordionSummary,
    AccordionDetails,
    Typography,
    TextField,
    Box,
    Container,
} from '@mui/material';
import ExpandMoreIcon from '@mui/icons-material/ExpandMore';

const PatientData = () => {
    const [patients, setPatients] = useState([]);
    const [patientIdInput, setPatientIdInput] = useState('');


    const fetchData = async () => {
        try {
            const token = localStorage.getItem("token");
            const response = await fetch('https://localhost:7083/patients');
            const data = await response.json();
            setPatients(data);
        } catch (error) {
            console.error('Error fetching data:', error);
        }
    };

    const fetchPatientById = async () => {
        try {
            const token = localStorage.getItem("token");
            const response = await fetch(`https://localhost:7083/patients/${patientIdInput}`);
            const data = await response.json();
            setPatients([data]);
        } catch (error) {
            console.error('Error fetching patient:', error);
        }
    };

    return (
        <Container maxWidth="md">
            <Box display="flex" justifyContent="center" alignItems="center" marginBottom="20px" marginTop="20px">
                <Button variant="contained" color="primary" onClick={fetchData} style={{ marginRight: '20px' }}>
                    Fetch Patient Data
        </Button>
                <Box display="flex" alignItems="center">
                    <TextField
                        label="Patient Encrypted ID"
                        value={patientIdInput}
                        onChange={(e) => setPatientIdInput(e.target.value)}
                        style={{ marginRight: '20px' }}
                    />
                    <Button variant="contained" color="secondary" onClick={fetchPatientById}>
                        Submit
          </Button>
                </Box>
            </Box>

            <TableContainer component={Paper}>
                <Table>
                    <TableHead>
                        <TableRow>
                            <TableCell>Patient Information</TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {patients.map((patient) => (
                            <React.Fragment key={patient.patientId}>
                                <TableRow>
                                    <TableCell colSpan={2}>
                                        <Accordion>
                                            <AccordionSummary expandIcon={<ExpandMoreIcon />}>
                                                <Typography>
                                                    First Name: {patient.firstName} - Last Name: {patient.lastName}
                                                </Typography>
                                            </AccordionSummary>
                                            <AccordionDetails>
                                                <TableContainer component={Paper}>
                                                    <Table>
                                                        <TableBody>
                                                            <TableRow>
                                                                <TableCell>Patient Encrypted ID</TableCell>
                                                                <TableCell>{patient.patientId}</TableCell>
                                                            </TableRow>
                                                            <TableRow>
                                                                <TableCell>First Name</TableCell>
                                                                <TableCell>{patient.firstName}</TableCell>
                                                            </TableRow>
                                                            <TableRow>
                                                                <TableCell>Last Name</TableCell>
                                                                <TableCell>{patient.lastName}</TableCell>
                                                            </TableRow>
                                                            <TableRow>
                                                                <TableCell>Gender</TableCell>
                                                                <TableCell>{patient.gender}</TableCell>
                                                            </TableRow>
                                                            <TableRow>
                                                                <TableCell>Date of Birth</TableCell>
                                                                <TableCell>{patient.dateOfBirth}</TableCell>
                                                            </TableRow>
                                                            <TableRow>
                                                                <TableCell>Address Line 1</TableCell>
                                                                <TableCell>{patient.addressLine1}</TableCell>
                                                            </TableRow>
                                                            <TableRow>
                                                                <TableCell>Address Line 2</TableCell>
                                                                <TableCell>{patient.addressLine2}</TableCell>
                                                            </TableRow>
                                                            <TableRow>
                                                                    <TableCell>City</TableCell>
                                                                    <TableCell>{patient.city}</TableCell>
                                                                </TableRow>
                                                                <TableRow>
                                                                    <TableCell>State</TableCell>
                                                                    <TableCell>{patient.state}</TableCell>
                                                                </TableRow>
                                                                <TableRow>
                                                                    <TableCell>Postal Code</TableCell>
                                                                    <TableCell>{patient.postalCode}</TableCell>
                                                                </TableRow>
                                                        </TableBody>
                                                    </Table>
                                            </TableContainer>
                      </AccordionDetails>
                    </Accordion>
                  </TableCell>
                </TableRow>
              </React.Fragment>
            ))}
          </TableBody>
        </Table>
      </TableContainer>
    </Container>
  );
};

export default PatientData;
