import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { Formik, Form, Field } from 'formik';
import { TextField } from 'formik-material-ui';
import { Button, Container, Typography, Box, Alert } from '@mui/material';

const LoginPage = () => {
    const [errorMessage, setErrorMessage] = useState('');
    const navigate = useNavigate();

    const handleSubmit = async (values, { setSubmitting }) => {
        try {
            const response = await fetch('https://localhost:7083/login', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(values),
            });

            if (response.ok) {
                const data = await response.json();
                console.log('Token:', data.token);
                localStorage.setItem('token', data.token);
                navigate('/home');
            } else {
                setErrorMessage('Invalid email or password');
            }
        } catch (error) {
            setErrorMessage('Error during authentication: ' + error.message);
        } finally {
            setSubmitting(false);
        }
    };

    return (
        <Container maxWidth="xs">
            <Box marginTop={4}>
                <Typography variant="h4" component="h1" align="center">
                    Login
                </Typography>
                <Formik
                    initialValues={{
                        email: '',
                        password: '',
                    }}
                    onSubmit={handleSubmit}
                >
                    {({ isSubmitting }) => (
                        <Form>
                            <Box marginBottom={2}>
                                <Field
                                    component={TextField}
                                    name="email"
                                    type="email"
                                    label="Email"
                                    fullWidth
                                    required
                                />
                            </Box>
                            <Box marginBottom={2}>
                                <Field
                                    component={TextField}
                                    name="password"
                                    type="password"
                                    label="Password"
                                    fullWidth
                                    required
                                />
                            </Box>
                            {errorMessage && (
                                <Box marginBottom={2}>
                                    <Alert severity="error">{errorMessage}</Alert>
                                </Box>
                            )}
                            <Box textAlign="center">
                                <Button type="submit" variant="contained" color="primary" disabled={isSubmitting}>
                                    Login
                                </Button>
                            </Box>
                        </Form>
                    )}
                </Formik>
            </Box>
        </Container>
    );
};

export default LoginPage;
