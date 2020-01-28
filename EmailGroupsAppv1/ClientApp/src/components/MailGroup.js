import React, { Fragment, useState } from 'react';
import { ListGroupItem, Collapse, Card, CardBody, Row, Col, Button, ButtonGroup, Table } from "reactstrap";
import Question from './Question';
import MailGroupEdit from './MailGroupEdit';
import PropTypes from "prop-types";
import MailAddressEdit from './MailAddressEdit';
import axios from 'axios';

export default function MailGroup(props) {
    const { mailGroup, isOpen, onClick, onDeleteGroup, onGroupEdit } = props;

    const [showDeleteQuestion, setShowDeleteQuestion] = useState(false);
    const [showGroupEditModal, setShowGroupEditModal] = useState(false);
    const toggleGroupEditModal = () => {
        setShowGroupEditModal(!showGroupEditModal);
    }

    const [showMailAddressEditModal, setShowMailAddressEditModal] = useState(false);
    const toggleMailAddressEditModal = () => {
        setShowMailAddressEditModal(!showMailAddressEditModal);
    }

    const deleteGroup = () => {
        axios.delete('api/MailGroups/' + mailGroup.id)
            .then(() => {
                onDeleteGroup(mailGroup.id);
                setShowDeleteQuestion(false)
            });
    }

    const removeMailAddress = mailAddress => {
        axios.delete('api/MailGroups/' + mailGroup.id + '/MailAddresses/' + mailAddress.id)
            .then(() => {

            })
    }

    // TODO
    //const removeMailAddress=

    return (
        <Fragment>
            <ListGroupItem tag='button' action onClick={onClick}>{mailGroup.name}</ListGroupItem>
            <Collapse isOpen={isOpen}>
                <Card>
                    <CardBody>
                        {mailGroup.description}
                        <Row>
                            <Col sm={12} className='text-center'>
                                <Button size='sm' onClick={() => { setShowMailAddressEditModal(!showMailAddressEditModal) }}>Add e-mail</Button>{' '}
                                <Button size='sm' onClick={toggleGroupEditModal}>Group details</Button>{' '}
                                <Button size='sm' onClick={() => { setShowDeleteQuestion(true) }}>Remove group</Button>
                            </Col>
                        </Row>
                        {mailGroup.addresses.length > 0 &&
                            <Table className={'mb-0'} striped size={'sm'} style={{ marginTop: '1em' }}>
                                <thead>
                                    <tr>
                                        <th>E-mail</th>
                                        <th>Name</th>
                                        <th>Last name</th>
                                        <th></th>
                                    </tr>
                                </thead>
                                <tbody>
                                    {mailGroup.addresses.map(mailAddress => (
                                        <tr key={mailAddress.id}>
                                            <td>{mailAddress.address}</td>
                                            <td>{mailAddress.name}</td>
                                            <td>{mailAddress.lastName}</td>
                                            <td>
                                                <ButtonGroup>
                                                    <Button size='sm' outline>Edit</Button>
                                                    <Button size='sm' outline>Remove</Button>
                                                </ButtonGroup>
                                            </td>
                                        </tr>
                                    ))}
                                </tbody>
                            </Table>}
                    </CardBody>
                </Card>
            </Collapse>

            <Question
                showModal={showDeleteQuestion}
                content={'Are you sure you want to delete that group?'}
                noClicked={() => { setShowDeleteQuestion(false) }}
                yesClicked={deleteGroup} />

            <MailGroupEdit
                showModal={showGroupEditModal}
                editedGroup={mailGroup}
                toggleModal={toggleGroupEditModal}
                onGroupEdit={onGroupEdit} />

            <MailAddressEdit
                showModal={showMailAddressEditModal}
                toggleModal={toggleMailAddressEditModal}
                mailGroup={mailGroup}
                onGroupEdit={onGroupEdit} />

        </Fragment>
    )
}

MailGroup.propTypes = {
    mailGroup: PropTypes.object.isRequired,
    isOpen: PropTypes.bool,
    onClick: PropTypes.func.isRequired,
    onDeleteGroup: PropTypes.func.isRequired,
    onGroupEdit: PropTypes.func.isRequired
}