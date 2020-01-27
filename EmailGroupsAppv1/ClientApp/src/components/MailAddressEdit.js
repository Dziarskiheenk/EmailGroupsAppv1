import React from "react";
import { Button, Modal, ModalHeader, ModalBody, ModalFooter, Form, FormGroup, Label, Input } from 'reactstrap';

export default function MailAddressEdit(props) {
    const { showModal } = props;

    return (
        <Modal isOpen={showModal}>
            <ModalHeader>TODO edit/add Mail Address</ModalHeader>
            <ModalBody>
                <Form>
                    <FormGroup>
                        <Label for="mailAddress">E-mail</Label>
                        <Input
                            type="email"
                            id="mailAddress"
                        //defaultValue={mailGroup.name}
                        // onChange={e => {
                        //     setMailGroup({ ...mailGroup, name: e.target.value });
                        // }} 
                        />
                        <Label for="mailAddressName">Name</Label>
                        <Input
                            type="text"
                            id="mailAddressName"
                        // defaultValue={mailGroup.description}
                        // onChange={e => {
                        //     setMailGroup({ ...mailGroup, description: e.target.value });
                        // }} 
                        />
                        <Label for="mailAddressLastName">Last name</Label>
                        <Input type="text" id="mailAddressLastName" />
                    </FormGroup>
                </Form>
            </ModalBody>
            <ModalFooter>
                <Button color={'secondary'} >Cancel</Button>
                <Button color={'success'} >Save</Button>
            </ModalFooter>
        </Modal>
    )
}